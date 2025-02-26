import { Injectable } from "@angular/core";
import { BehaviorSubject, Observable } from "rxjs";
import { User, CreateUserDTO } from "../models/user.model";
import { jwtDecode } from "jwt-decode";
import { Constant } from "../constants";

@Injectable({
  providedIn: "root",
})
export class UserService {
  private users = new BehaviorSubject<User[]>([
    {
      id: "00000000-0000-0000-0000-000000000001".trim(),
      firstName: "John",
      lastName: "Smith",
      email: "john.smith@example.com",
      role: "admin",
      status: "active",
      permissions: {
        viewProperties: true,
        manageProperties: true,
        viewFinancial: true,
        manageFinancial: true,
        viewMaintenance: true,
        manageMaintenance: true,
      },
      createdAt: "2024-01-15T10:30:00Z",
      updatedAt: "2024-01-15T10:30:00Z",
    },
  ]);

  getUsers(): Observable<User[]> {
    return this.users.asObservable();
  }

  createUser(data: CreateUserDTO): void {
    const currentUsers = this.users.getValue();
    const newUser: User = {
      ...data,
      status: "pending",
      createdAt: new Date().toISOString(),
      updatedAt: new Date().toISOString(),
    };

    this.users.next([...currentUsers, newUser]);
  }

  updateUserStatus(id: string, status: User["status"]): void {
    const currentUsers = this.users.getValue();
    const updatedUsers = currentUsers.map((user) =>
      user.id === id
        ? { ...user, status, updatedAt: new Date().toISOString() }
        : user
    );

    this.users.next(updatedUsers);
  }

  deleteUser(id: string): void {
    const currentUsers = this.users.getValue();
    this.users.next(currentUsers.filter((user) => user.id !== id));
  }
  public getToken(): string | undefined {
    const token = localStorage.getItem(Constant.token);
    if (!token) {
      console.warn("Token is missing");
      return undefined;
    }
    return token;
  }

  public getCurrentOrganizationId(): string | undefined {
    const jwtToken = jwtDecode<User>(
      localStorage.getItem(Constant.token) ?? ""
    );
    return jwtToken.organizationId;
  }

  public getCurrentUserId(): string | undefined {
    const jwtToken = jwtDecode<User>(
      localStorage.getItem(Constant.token) ?? ""
    );
    return jwtToken.id;
  }
}
