import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { User, CreateUserDTO } from '../types/user.types';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  private users = new BehaviorSubject<User[]>([
    {
      id: 1,
      firstName: 'John',
      lastName: 'Smith',
      email: 'john.smith@example.com',
      role: 'admin',
      status: 'active',
      permissions: {
        viewProperties: true,
        manageProperties: true,
        viewFinancials: true,
        manageFinancials: true,
        viewMaintenance: true,
        manageMaintenance: true
      },
      createdAt: '2024-01-15T10:30:00Z',
      updatedAt: '2024-01-15T10:30:00Z'
    }
  ]);

  getUsers(): Observable<User[]> {
    return this.users.asObservable();
  }

  createUser(data: CreateUserDTO): void {
    const currentUsers = this.users.getValue();
    const newUser: User = {
      ...data,
      id: Math.max(0, ...currentUsers.map(u => u.id)) + 1,
      status: 'pending',
      createdAt: new Date().toISOString(),
      updatedAt: new Date().toISOString()
    };
    
    this.users.next([...currentUsers, newUser]);
  }

  updateUserStatus(id: number, status: User['status']): void {
    const currentUsers = this.users.getValue();
    const updatedUsers = currentUsers.map(user => 
      user.id === id 
        ? { ...user, status, updatedAt: new Date().toISOString() }
        : user
    );
    
    this.users.next(updatedUsers);
  }

  deleteUser(id: number): void {
    const currentUsers = this.users.getValue();
    this.users.next(currentUsers.filter(user => user.id !== id));
  }
}