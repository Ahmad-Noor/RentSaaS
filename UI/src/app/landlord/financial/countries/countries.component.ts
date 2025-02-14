import { Component, OnInit, ViewChild } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource, MatTableModule } from '@angular/material/table'; 
import { CountryAddEditComponent } from './country-add-edit/country-add-edit.component';  
import { MatToolbarModule } from '@angular/material/toolbar';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';
import { RouterLink } from '@angular/router';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatPaginatorModule } from '@angular/material/paginator';
import { CountriesService } from '../../../service/countries.service';
import { CoreService } from '../../../core/core.service';

@Component({
  selector: 'app-countries',
   standalone: true, 
   imports: [
    CommonModule,
    ReactiveFormsModule, 
    MatToolbarModule,
    MatFormFieldModule,
    MatTableModule,
    MatIconModule,
    MatPaginatorModule,
  ],
  templateUrl: './countries.component.html',
  styleUrls: ['./countries.component.scss']
})



export class CountriesComponent implements OnInit {
  displayedColumns: string[] = [
    'id',
    'countryName',
    'createDate',
    'updateDate',
    'action',
  ];
  dataSource!: MatTableDataSource<any>;
   

  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;

  constructor(
    private _dialog: MatDialog,
    private _countriesService: CountriesService,
    private _coreService: CoreService
  ) { }

  ngOnInit(): void {
    this.getCountriesList();
  }

  openAddEditEmpForm() {
    const dialogRef = this._dialog.open(CountryAddEditComponent);
    dialogRef.afterClosed().subscribe({
      next: (val) => {
        if (val) {
          this.getCountriesList();
        }
      },
    });
  }

  getCountriesList() {
    this._countriesService.getAllCountries().subscribe({
      next: (res) => {
        this.dataSource = new MatTableDataSource(res);
        this.dataSource.sort = this.sort;
        this.dataSource.paginator = this.paginator;

      },
      error: console.log,
    });


  }

  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();

    if (this.dataSource.paginator) {
      this.dataSource.paginator.firstPage();
    }
  }

  deleteEmployee(id: string) {
    this._countriesService.deleteCountry(id).subscribe({
      next: (res) => {
        this._coreService.openSnackBar('Country deleted!', 'done');
        this.getCountriesList();
      },
      error: console.log,
    });
  }

  openEditForm(data: any) {
    // console.log(id);
    // this._countrysService.getCountryById(id).subscribe({ next: (res) => { this.country = res; }, error: console.log, });

    // console.log(this.country);

    const dialogRef = this._dialog.open(CountryAddEditComponent, { data, });

    dialogRef.afterClosed().subscribe({
      next: (val) => {
        if (val) {
          this.getCountriesList();
        }
      },
    });
  }
}