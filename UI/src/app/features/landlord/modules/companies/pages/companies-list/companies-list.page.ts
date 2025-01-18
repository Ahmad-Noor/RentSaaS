import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { RouterLink } from '@angular/router';
import { DataTableComponent, TableColumn } from '../../../../../../shared/components/data-table/data-table.component';
import { ActionBarComponent } from '../../../../../../shared/components/action-bar/action-bar.component';
import { CompaniesService } from '../../services/companies.service';
import { Company } from '../../types/company.types';
import { ConfirmDialogService } from '../../../../../../shared/services/confirm-dialog/confirm-dialog.service';

@Component({
  selector: 'app-companies-list-page',
  standalone: true,
  imports: [CommonModule, RouterLink, DataTableComponent, ActionBarComponent],
  template: `
    <div class="space-y-6">
      <div class="flex justify-between items-center">
        <h1 class="text-2xl font-semibold">Companies</h1>
        <a 
          routerLink="new"
          class="bg-[#0078D4] text-white px-4 py-2 rounded flex items-center gap-2 hover:bg-[#106EBE] transition-colors"
        >
          <span class="material-icons text-sm">add</span>
          Add Company
        </a>
      </div>

      <div class="bg-white rounded-lg shadow">
        <div class="p-6">
          <app-action-bar 
            searchPlaceholder="Search companies" 
            (onSearch)="handleSearch($event)"
          />
          <app-data-table 
            [columns]="columns"
            [data]="filteredCompanies"
            (onAction)="handleAction($event)"
          />
        </div>
      </div>
    </div>
  `
})
export class CompaniesListPage implements OnInit {
  columns: TableColumn[] = [
    { key: 'name', label: 'Company Name' },
    { key: 'type', label: 'Type' },
    { key: 'properties', label: 'Properties' },
    { key: 'employees', label: 'Employees' },
    { key: 'status', label: 'Status', type: 'status' },
    { key: 'actions', label: 'Actions', type: 'actions' }
  ];

  companies: Company[] = [];
  filteredCompanies: Company[] = [];

  constructor(
    private companiesService: CompaniesService,
    private confirmDialog: ConfirmDialogService,
    private router: Router
  ) {}

  ngOnInit() {
    this.companiesService.getCompanies().subscribe(companies => {
      this.companies = companies;
      this.filteredCompanies = companies;
    });
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
          this.companiesService.deleteCompany(action.item.id);
        }
        break;
    }
  }
}