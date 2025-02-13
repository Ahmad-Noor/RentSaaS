import { Component, OnInit, ViewChild } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatPaginator, MatPaginatorModule } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource, MatTableModule } from '@angular/material/table'; 
import { AddressAddEditComponent } from './address-add-edit/address-add-edit.component';  
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { AddressService } from '../../../service/address.service';
import { CoreService } from '../../../core/core.service';

@Component({
  selector: 'app-address',
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
  templateUrl: './address.component.html',
  styleUrls: ['./address.component.scss']
})



export class AddressComponent implements OnInit {
  displayedColumns: string[] = [
    'id',
    'street',
    'line2',
    'city',
    'createDate',
    'updateDate',
    'action',
  ];
  dataSource!: MatTableDataSource<any>;
   

  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;

  constructor(
    private _dialog: MatDialog,
    private _addressService: AddressService,
    private _coreService: CoreService
  ) { }

  ngOnInit(): void {
    this.getAddressList();
  }

  openAddEditEmpForm() {
    const dialogRef = this._dialog.open(AddressAddEditComponent);
    dialogRef.afterClosed().subscribe({
      next: (val) => {
        if (val) {
          this.getAddressList();
        }
      },
    });
  }

  getAddressList() {
    this._addressService.getAllAddress().subscribe({
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
    this._addressService.deleteAddress(id).subscribe({
      next: (res) => {
        this._coreService.openSnackBar('Address deleted!', 'done');
        this.getAddressList();
      },
      error: console.log,
    });
  }

  openEditForm(data: any) {
    // console.log(id);
    // this._addresssService.getAddressById(id).subscribe({ next: (res) => { this.address = res; }, error: console.log, });

    // console.log(this.address);

    const dialogRef = this._dialog.open(AddressAddEditComponent, { data, });

    dialogRef.afterClosed().subscribe({
      next: (val) => {
        if (val) {
          this.getAddressList();
        }
      },
    });
  }
}