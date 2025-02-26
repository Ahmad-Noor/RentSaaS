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
import { Companies } from "../../../../models/companies";
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
  company!: Companies[];

  constructor(
    private _fss: FormBuilder,
    private router: Router,
    private route: ActivatedRoute,
    private _propertyServices: PropertyService,
    private _expenseService: ExpenseService,
    private _CompanyService: CompanyService
  ) {
    this.getAllProperties();

    this.expenseForm = this._fss.group({
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
  // ngOnInit() {
  //   this.getCompany();

  //   const expenseData = history.state.expense;
  //   if (expenseData) {
  //     // Patch form values with the expense data so that the form is pre-populated for editing
  //     this.expenseForm.patchValue(expenseData);
  //   }
  // }
  ngOnInit(): void {
    this.getCompany();
    this.getAllProperties();
  
    // Retrieve the passed expense from the router state
    const expenseData = history.state.expense;
    if (expenseData) {
      // Convert the dueDate to ISO format "YYYY-MM-DD" if necessary.
      if (expenseData.dueDate) {
        expenseData.dueDate = new Date(expenseData.dueDate)
          .toISOString()
          .substring(0, 10);
      }
      // Patch form values with the expense data so that the form is pre-populated for editing
      this.expenseForm.patchValue(expenseData);
    }
  }
  onFormSubmit(): void {
    if (this.expenseForm.valid) {
      const expenseData = this.expenseForm.value;
      if (expenseData.id) {
        // If an ID is present, assume editing and update the expense
        this._expenseService
          .updateExpense(expenseData.id, expenseData)
          .subscribe({
            next: (val: any) => {
              console.log("Expense updated successfully");
            },
            error: (err: any) => {
              console.error("Error updating expense", err);
            },
          });
      } else {
        // Otherwise, add a new expense
        this._expenseService.addExpense(expenseData as Expense).subscribe({
          next: (val: any) => {
            console.log("Expense added successfully");
          },
          error: (err: any) => {
            console.error("Error adding expense", err);
          },
        });
      }
    }

    this.router.navigate([".."], { relativeTo: this.route });
  }
  getCompany() {
    this._CompanyService.getCompanies().subscribe({
      next: (result) => {
        this.company = result.data;
      },
      error: (result) => {},
    });
  }

  getAllProperties() {
    this._propertyServices.getAllProperties().subscribe({
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

    files.forEach((file) => {
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
    return { isValid: false, error: "Invalid file type" };
  }

  if (file.size > maxSize) {
    return { isValid: false, error: "File size exceeds 5MB" };
  }

  return { isValid: true };
}
