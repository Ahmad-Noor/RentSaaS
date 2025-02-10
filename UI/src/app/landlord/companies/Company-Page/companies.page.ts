import { Component, EventEmitter, Inject, inject, Input, OnChanges, OnInit, Output, PLATFORM_ID, SimpleChanges } from '@angular/core';
import { Router, RouterLink } from '@angular/router';
import { ConfirmDialogService } from '../../../shared/services/confirm-dialog/confirm-dialog.service';
import { CompaniesService } from '../services/companies.service';
import { Company } from '../types/company.types';
import { CommonModule, isPlatformBrowser } from '@angular/common';
import { ActionBarComponent } from '../../../shared/components/action-bar/action-bar.component';
import { platformBrowser } from '@angular/platform-browser';

@Component({
  selector: 'app-companies-page',
  imports: [CommonModule, RouterLink, ActionBarComponent],
  standalone: true,
  templateUrl: `./companies.page.html`
})
export class CompaniesPage implements OnInit, OnChanges {
  @Input() data!: Company[];   
  @Output() onAction = new EventEmitter<{ type: string; item: Company }>();
  

  columns: Array<{ key: keyof Company | 'actions'; label: string; type?: string }> = [
    { key: 'name', label: 'Company Name' },
    { key: 'type', label: 'Type' },
    { key: 'ein', label: 'ein' },
    // { key: 'employees', label: 'Employees' },
    // { key: 'status', label: 'Status', type: 'status' },
    { key: 'actions', label: 'Actions', type: 'actions' } // Handle actions separately in template
  ];
  

  companies: Company[] = [];
  filteredCompanies: Company[] = [];
  platformId=inject(PLATFORM_ID);



  constructor(
    private companiesService: CompaniesService,
    private confirmDialog: ConfirmDialogService,
    private router: Router,
 
  ) {




  }

  ngOnInit() {
    if(isPlatformBrowser(this.platformId))
    {
      this.getData();
    }

  }




  getData(){
    this.companiesService.getCompanies().subscribe({
      next: companies => {

        this.companies = companies.data;
        this.filteredCompanies = companies.data;;
        console.log(companies.data)
      },
      error: error => {
        console.error('Error fetching companies', error);
      },
      complete:()=>{console.log('completed')}
    }
    );
  }







  ngOnChanges(changes: SimpleChanges) {
    // Whenever the input data changes, reset filteredCompanies
    if (changes['data']) {
      this.filteredCompanies = this.data;
    }
  }

  handleSearch(term: string) {
    this.filteredCompanies = this.companies.filter(company => 
      company.name.toLowerCase().includes(term.toLowerCase()) ||
      company.type.toLowerCase().includes(term.toLowerCase())
    );
  }

  async handleAction(action: { type: string; item: Company }) {
    switch (action.type) {
      case 'edit':
        this.router.navigate(['landlord', 'companies', action.item.id, 'edit']);
        break;

      case 'delete':
        const confirmed = await this.confirmDialog.show({
          title: 'Delete Company',
          message: `Are you sure you want to delete ${action.item.name}?`,
          confirmText: 'Delete',
          cancelText: 'Cancel',
          type: 'danger'
        });

        if (confirmed) {
            this.companiesService.deleteCompany(action.item.id).subscribe({
              next:()=>{console.log("delete Company Is Done Succes")},
            });
        }
        break;
    }
  }

  getStatusClass(status: string | undefined): string {
    if (!status) return 'bg-gray-100 text-gray-800';
    
    const baseClasses = 'px-2 py-1 rounded-full text-sm';
    const statusClasses: Record<string, string> = {
      'Active': 'bg-green-100 text-green-800',
      'Inactive': 'bg-gray-100 text-gray-800',
      'Pending': 'bg-yellow-100 text-yellow-800',
      'Completed': 'bg-blue-100 text-blue-800'
    };

    return `${baseClasses} ${statusClasses[status] || 'bg-gray-100 text-gray-800'}`;
  }
}