import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink, ActivatedRoute, Router } from '@angular/router';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { CompanyService } from '../../services/company.service';

@Component({
  selector: 'app-company-form-page',
  standalone: true,
  imports: [CommonModule, RouterLink, ReactiveFormsModule],
  templateUrl: './company-form.html'
})
export class CompanyFormPage implements OnInit {
  companyForm: FormGroup;
  isEditMode = false;
  loading = false;
  companyId?: number;

  constructor(
    private fb: FormBuilder,
    private companyService: CompanyService,
    private route: ActivatedRoute,
    private router: Router
  ) {
    this.companyForm = this.fb.group({
      name: ['', [Validators.required]],
      type: ['', [Validators.required]],
      properties: ['', [Validators.required]],
      employees: ['', [Validators.required]],
      description: [''],
      status: ['Active']
    });
  }

  ngOnInit() {
    const id = this.route.snapshot.paramMap.get('id');
    if (id) {
      this.isEditMode = true;
      this.companyId = parseInt(id, 10);
      this.loadCompany(this.companyId);
    }
  }

  private loadCompany(id: number) {
    this.loading = true;
    this.companyService.getCompany(id).subscribe({
      next: (company) => {
        if (company) {
          this.companyForm.patchValue(company);
        }
      },
      error: (error) => {
        console.error('Error loading company:', error);
      },
      complete: () => {
        this.loading = false;
      }
    });
  }

  onSubmit() {
    if (this.companyForm.valid) {
      this.loading = true;
      const data = this.companyForm.value;

      const request = this.isEditMode && this.companyId
        ? this.companyService.updateCompany(this.companyId, data)
        : this.companyService.createCompany(data);

      request.subscribe({
        next: () => {
          this.router.navigate(['..'], { relativeTo: this.route });
        },
        error: (error) => {
          console.error('Error saving company:', error);
          this.loading = false;
        }
      });
    }
  }
}