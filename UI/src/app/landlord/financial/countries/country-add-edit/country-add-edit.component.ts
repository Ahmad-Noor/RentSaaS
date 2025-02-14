import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';  
import { CommonModule } from '@angular/common';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatTableModule } from '@angular/material/table';
import { MatIconModule } from '@angular/material/icon';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatDialogModule } from '@angular/material/dialog';
import { CountriesService } from '../../../../service/countries.service';
import { CoreService } from '../../../../core/core.service';
import { Country } from '../../../../models/country.model';
  
@Component({
  selector: 'app-country-add-edit',   
  standalone: true, 
     imports: [
      CommonModule,
      ReactiveFormsModule, 
      MatToolbarModule,
      MatFormFieldModule,
      MatTableModule,
      MatIconModule,
      MatPaginatorModule,
      MatDialogModule
    ],
  templateUrl: './country-add-edit.component.html',
  styleUrls: ['./country-add-edit.component.scss']
})
export class CountryAddEditComponent implements OnInit {
  countryForm: FormGroup;
 

  constructor(
    private _fss: FormBuilder,
    private _countriesService: CountriesService,
 
    private _dialogRef: MatDialogRef<CountryAddEditComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any,
    private _coreService: CoreService
  ) {

 
    this.countryForm = this._fss.group({
      id: 0,
      countryName: '',
      createBy: 0,
      updatedBy: 0,
      
    });
  }



  ngOnInit(): void {
    this.countryForm.patchValue(this.data);
  }

  onFormSubmit() {
    if (this.countryForm.valid) {
      if (this.data) {
        this._countriesService
          .updateCountry(this.data.id, this.countryForm.value)
          .subscribe({
            next: (val: any) => {
              this._coreService.openSnackBar('Country detail updated!');
              this._dialogRef.close(true);
            },
            error: (err: any) => {
              console.error(err);
            },
          });
      } else {
        this._countriesService.addCountry(this.countryForm.value as Country).subscribe({
          next: (val: any) => {
            this._coreService.openSnackBar('Country added successfully');
            this._dialogRef.close(true);
          },
          error: (err: any) => {
            console.error(err);
          },
        });

      };
    }
  }
}


