import { RouterLink, Router, ActivatedRoute } from "@angular/router";
import { ExpenseService } from "../../../../service/expense.service";
import { ExpenseFormData } from "../../../../models/expense-form.types";
import { EventEmitter, Input, Output, OnInit, Component } from "@angular/core";
import { CommonModule } from "@angular/common";
import {
  FormBuilder,
  FormControl,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from "@angular/forms";
import { Expense } from "../../../../models/expense.types";
import { Receipt } from "../../../../models/receipt.types";
import { PropertyService } from "../../../../service/property.service"; 
import { Company } from "../../../../models/company.types";
import { CompanyService } from "../../../../service/company.service";
import { ReceiptItemComponent } from "./receipt-item.component"; 

@Component({
  selector: "app-add-expense-page",
  standalone: true,
  imports: [
    RouterLink,
    CommonModule,
    ReactiveFormsModule,
    ReceiptItemComponent,
  ],
  templateUrl: "./add-expense.page.html",
})
export class AddExpensePage implements OnInit {
  expenseForm: FormGroup;
  @Input() expense?: Expense;
  @Output() save = new EventEmitter<ExpenseFormData>();

  receipts: Receipt[] = [];
  error = "";
  loading = false;
  properties: any[] = [];
  company!: Company[];

  constructor(
    private _fb: FormBuilder,
    private router: Router,
    private route: ActivatedRoute,
    private _propertyService: PropertyService,
    private _expenseService: ExpenseService,
    private _companyService: CompanyService
  ) { 

    this.expenseForm = this._fb.group({
      id: "",
      propertyId: new FormControl(null, [Validators.required]),
      paymentSchedule: new FormControl(null, [Validators.required]),
      category: new FormControl(null, [Validators.required]),
      expenseType: new FormControl("property"),
      amount: new FormControl(0, [Validators.required]),
      dueDate: new FormControl(null, [Validators.required]),
      details: new FormControl(null),
      isPaid: new FormControl(true, Validators.required),
      type: new FormControl("property"),
      CompanyId: new FormControl(null),
      receipts: new FormControl([]),
    });
  }

  ngOnInit(): void {
    this.getCompany();
    this.getAllProperties();
  
    // Check if we're editing an existing expense
    const expenseData = history.state.expense;
    if (expenseData) {
    if (expenseData.dueDate) {
      expenseData.dueDate = new Date(expenseData.dueDate)
        .toISOString()
        .substring(0, 10);
    }
    this.expenseForm.patchValue(expenseData);
    }
  }
  onFormSubmit(): void {
    if (this.expenseForm.valid) {
      const expenseData = this.expenseForm.value;
    
    // Format date for API
    if (expenseData.dueDate) {
      const date = new Date(expenseData.dueDate);
      if (!isNaN(date.getTime())) {
        expenseData.dueDate = date.toISOString().split('T')[0];
      }
    }
    
    // Prepare receipt files
    const files = this.receipts.map(receipt => receipt.file);
    
    // Update or create expense
    if (expenseData.id) {
        this._expenseService
          .updateExpense(expenseData.id, expenseData, files)
      .subscribe({
            next: (val: any) => {
              console.log("Expense updated successfully");
              this.router.navigate([".."], { relativeTo: this.route });
        },
            error: (err: any) => {
              console.error("Error updating expense", err);
              // Add error handling here to display to user
            },
          });
      } else {
        // For adding new expense
        this._expenseService.addExpense(expenseData, files).subscribe({
          next: (val: any) => {
            console.log("Expense added successfully", val);
            this.router.navigate([".."], { relativeTo: this.route });
        },
          error: (err: any) => {
            console.error("Error adding expense", err);
            if (err.error && err.error.message) {
              this.error = err.error.message;
            } else {
              this.error = "Failed to add expense. Please try again.";
            }
          },
        });
      }
    } else {
      // Mark all form controls as touched to show validation errors
      Object.keys(this.expenseForm.controls).forEach(key => {
        const control = this.expenseForm.get(key);
        control?.markAsTouched();
      });
  }
  }
  getCompany() {
    this._companyService.getCompanies().subscribe({
      next: (result) => {
        this.company = result.data;
        },
      error: (result) => {},
    });
  }

  getAllProperties() {
    this._propertyService.getAllProperties().subscribe({
      next: (properties: any) => {
        this.properties = properties.data;
      },
      error: (properties) => {},
      complete: () => {},
    });
  }

  onFilesSelected(event: any): void {
    const files = Array.from((event.target as HTMLInputElement).files || []);
    
    if (this.receipts.length + files.length > 5) {
      this.error = "You can upload a maximum of 5 receipts";
      return;
    }

    this.error = ""; // Clear any previous errors
    
    files.forEach((file: any) => {
      const validation = validateReceipt(file);
      if (!validation.isValid) {
        this.error = validation.error || "Invalid file";
        return;
      }

      const receipt: Receipt = {
        id: crypto.randomUUID(),
        file,
        name: file.name,
        size: file.size,
        type: file.type,
      };

      this.receipts.push(receipt);
    });

    // Reset the file input
    (event.target as HTMLInputElement).value = "";
  }

  removeReceipt(receipt: any): void {
    this.receipts = this.receipts.filter((r) => r.id !== receipt.id);
    this.error = "";
  }
}

function validateReceipt(file: File): { isValid: boolean; error?: string } {
  const validTypes = ["image/jpeg", "image/png", "application/pdf"];
  const maxSize = 5 * 1024 * 1024; // 5MB

  if (!validTypes.includes(file.type)) {
    return { isValid: false, error: "Invalid file type. Please upload JPG, PNG or PDF only." };
  }

  if (file.size > maxSize) {
    return { isValid: false, error: "File size exceeds 5MB limit." };
  }

  return { isValid: true };
}