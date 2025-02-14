import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA, MatDialogModule } from '@angular/material/dialog';   
import { CommonModule } from '@angular/common';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatTableModule } from '@angular/material/table';
import { MatIconModule } from '@angular/material/icon';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatSelectModule } from '@angular/material/select';
import { AddressService } from '../../../../service/address.service';
import { CountriesService } from '../../../../service/countries.service';
import { Country } from '../../../../models/country.model';
import { CoreService } from '../../../../core/core.service';
import { Address } from '../../../../models/address.model';

@Component({
  selector: 'app-address-add-edit',
     standalone: true, 
     imports: [
      CommonModule,
      ReactiveFormsModule, 
      MatToolbarModule,
      MatFormFieldModule,
      MatTableModule,
      MatIconModule,
      MatPaginatorModule,
      MatSelectModule,
      MatDialogModule
    ],
  templateUrl: './address-add-edit.component.html',
  styleUrls: ['./address-add-edit.component.scss']
})
export class AddressAddEditComponent implements OnInit {
  addressForm: FormGroup;
  countries: Country[] = [];


  constructor(
    private _fss: FormBuilder,
    private _addressService: AddressService,
    private _countryService: CountriesService,

    private _dialogRef: MatDialogRef<AddressAddEditComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any,
    private _coreService: CoreService
  ) {

    _countryService.getAllCountries().subscribe({ next: (res) => { this.countries = res; }, error: console.log, });

    this.addressForm = this._fss.group({
      id: 0,
      note: '',
      street: '',
      line2: '',
      city: '',
      state: '',
      postalCode: '',
      isActive:false,
      countryId:0, 
      createBy: 0,
      updatedBy: 0,
      
    });
  }



  ngOnInit(): void {
    this.addressForm.patchValue(this.data);
  }

  onFormSubmit() {
    if (this.addressForm.valid) {
      if (this.data) {
        this._addressService
          .updateAddress(this.data.id, this.addressForm.value)
          .subscribe({
            next: (val: any) => {
              this._coreService.openSnackBar('Address detail updated!');
              this._dialogRef.close(true);
            },
            error: (err: any) => {
              console.error(err);
            },
          });
      } else {
        this._addressService.addAddress(this.addressForm.value as Address).subscribe({
          next: (val: any) => {
            this._coreService.openSnackBar('Address added successfully');
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


