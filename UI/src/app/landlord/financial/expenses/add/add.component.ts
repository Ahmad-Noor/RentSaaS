import {
  Component,
  EventEmitter,
  Input,
  Output,
  OnInit,
  Inject,
  PLATFORM_ID,
} from "@angular/core";
import { CommonModule, isPlatformBrowser } from "@angular/common";
import {
  FormBuilder,
  FormControl,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from "@angular/forms";
import { RouterLink } from "@angular/router";
import { ExpenseFormData } from "./models/expense-form.model";
import { initializeExpenseForm } from "./utils/form-utils";
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { ReceiptItemComponent } from "./receipt-item.component";
import { FormFieldComponent } from "../../../../shared/components/form-field/form-field.component"; 
import { Expense } from "../../../../models/expense.types";
import { Receipt } from "../../../../models/receipt.types";
import { PropertyService } from "../../../../service/property.service";
import { ExpenseService } from "../../../../service/expense.service";

@Component({
  selector: "app-expense-form",
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    RouterLink,
    FormFieldComponent,
    ReceiptItemComponent,
  ],
  templateUrl: "./add.component.html",
})
export class AddComponent implements OnInit {
  @Input() expense?: Expense;
  @Output() save = new EventEmitter<ExpenseFormData>();

  receipts: Receipt[] = [];
  error = "";

  expenseForm: FormGroup;
  loading = false;
  properties: any[] = [];

  DataForm = new FormGroup({
    propertyId: new FormControl("onetime", [Validators.required]),
    paymentSchedule: new FormControl(0,[Validators.required]),
    category: new FormControl(0,[Validators.required]),
    expenseType: new FormControl("property"),
    amount: new FormControl(null),
    dueDate: new FormControl(null),
    details: new FormControl(null),
    isPaid: new FormControl(true, Validators.required),
    type: new FormControl("property"),
    receipts: new FormControl([]),
  });

  constructor(
    private fb: FormBuilder,
    private _propertyServices: PropertyService,
    @Inject(PLATFORM_ID) private platformId: Object,
    private _httpClient: HttpClient,
    private _expenseService: ExpenseService
  ) {
    this.expenseForm = initializeExpenseForm(fb);

    if (isPlatformBrowser(platformId)) {
      this.getAllProperties();
      this._expenseService.getAllExpenses().subscribe((resulte) => {
        console.log(resulte);
      });
    }
  }

  ngOnInit() {
    if (this.expense) {
      this.expenseForm.patchValue({
        type: this.expense.type || "property",
        propertyId: this.expense.propertyId,
        category: this.expense.category,
        expenseType: this.expense.recurring ? "recurring" : "onetime",
        amount: this.expense.amount,
        dueDate: this.expense.dueDate,
        details: this.expense.description,
        isPaid: this.expense.status === "paid",
      });
    }
  }
  handleSubmit(data: FormGroup): void {
    if (data.invalid) {
      this.error = "Please fill out all required fields correctly.";
      return;
    }

    let formData = new FormData();
    formData.append("expenseType", data.get("expenseType")?.value);
    formData.append("propertyId", data.get("propertyId")?.value);
    formData.append("category", data.get("category")?.value);
    formData.append("amount", data.get("amount")?.value);
    formData.append("dueDate", data.get("dueDate")?.value);
    formData.append("details", data.get("details")?.value);
    formData.append("isPaid", data.get("isPaid")?.value);
    this.receipts.forEach((receipt) => {
      formData.append("ReceiptsFiles", receipt.file, receipt.name);
    });

    this.loading = true;
    this._expenseService.add(formData).subscribe({
      next: (result) => {
        console.log("result", result);
        this.save.emit(result);
        this.loading = false;
      },
      error: (error) => {
        console.log("Error", error);
        this.error = "An error occurred while saving the expense.";
        this.loading = false;
      },
      complete: () => {
        console.log("Complete");
      }
    });
  }







  getAllProperties() {
    this._propertyServices.getAllProperties().subscribe({
      next: (properties) => {
        this.properties = properties;
      },
      error: (properties) => {},
      complete: () => {},
    });
  }

  getFieldError(field: string): string {
    const control = this.DataForm.get(field);
    if (control?.touched && control.errors) {
      if (control.errors["required"]) {
        return `${field} is required`;
      }
      if (control.errors["min"]) {
        return `${field} must be greater than ${control.errors["min"].min}`;
      }
    }
    return "";
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