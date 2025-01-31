import { Component, EventEmitter, Output } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink, Router } from '@angular/router';
import { NgModel } from '@angular/forms';

interface User {
  id: number;
  name: string;
  email: string;
  role: string;
  status: string;
  [key: string]: any;
}

@Component({
  selector: 'app-users-list-page',
  standalone: true,
  imports: [CommonModule, RouterLink],
  templateUrl:'./Users-List.Page.html'  
})
export class UsersListPage {
  @Output() onAction = new EventEmitter<{ type: string; item: any }>();

  columns = [
    { key: 'name', label: 'Name' },
    { key: 'email', label: 'Email' },
    { key: 'role', label: 'Role' },
    { key: 'status', label: 'Status', type: 'status' },
    { key: 'actions', label: 'Actions', type: 'actions' }
  ];

  users: User[] = [
    { id: 1, name: 'John Doe', email: 'john@example.com', role: 'Administrator', status: 'Active' },
    { id: 2, name: 'Jane Smith', email: 'jane@example.com', role: 'User', status: 'Inactive' },
    { id: 3, name: 'Bob Johnson', email: 'bob@example.com', role: 'Moderator', status: 'Pending' }
  ];

  filterText = '';  // Bind the filter input to this variable
  filteredUsers: User[] = this.users;  // Start with all users as filtered

  constructor(private router: Router) {}

  trackByFn(index: number, item: any): number {
    return item.id;
  }

  getStatusClass(status: string): string {
    const baseClasses = 'px-2 py-1 rounded-full text-sm';
    const statusClasses: Record<string, string> = {
      'Active': 'bg-green-100 text-green-800',
      'Inactive': 'bg-gray-100 text-gray-800',
      'Pending': 'bg-yellow-100 text-yellow-800',
      'Completed': 'bg-blue-100 text-blue-800'
    };
    return `${baseClasses} ${statusClasses[status] || 'bg-gray-100 text-gray-800'}`;
  }

  handleAction(type: string, item: any): void {
    this.onAction.emit({ type, item });
  }

  handleSearch(event: Event): void {
    const inputElement = event.target as HTMLInputElement; // Cast to HTMLInputElement
    const term = inputElement.value;
    this.filteredUsers = this.users.filter(user =>
      user.name.toLowerCase().includes(term.toLowerCase()) ||
      user.email.toLowerCase().includes(term.toLowerCase())
    );
  }

  handleDelete(userId: number): void {
    // Filter out the deleted user from both filteredUsers and users
    this.users = this.users.filter(user => user.id !== userId);
    this.filteredUsers = this.filteredUsers.filter(user => user.id !== userId);
  }

  handleEdit(userId: number): void {
    // Navigate to the edit page with the userId as a route parameter
    this.router.navigate([`/edit/${userId}`]);
  }
}
