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
import { ExpenseDetailsFormComponent } from "./expense-details-form/expense-details-form.component"; 
import { Expense } from "../../types/expense.types";
import { ExpenseFormData } from "./models/expense-form.model";
import { initializeExpenseForm } from "./utils/form-utils";
import { FormFieldComponent } from "../../../../../shared/components/form-field/form-field.component";
import { PropertyService } from "../../../properties/services/property.service";
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Receipt } from "../../types/receipt.types";
import { ReceiptItemComponent } from "./receipt-item.component";
import { ExpenseService } from "../../services/expense.service";

@Component({
  selector: "app-expense-form",
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    RouterLink,
    ExpenseDetailsFormComponent, 
    FormFieldComponent,
    ReceiptItemComponent,
  ],
  templateUrl: "./expense-form.component.html",
})
export class ExpenseFormComponent implements OnInit {
  @Input() expense?: Expense;
  @Output() save = new EventEmitter<ExpenseFormData>();

  receipts: Receipt[] = [];
  error = "";

  expenseForm: FormGroup;
  loading = false;
  properties: any[] = [];

  DataForm = new FormGroup({
    propertyId: new FormControl(null, Validators.required),
    paymentSchedule: new FormControl(null),
    category: new FormControl(null),
    expenseType: new FormControl("property"),
    amount: new FormControl(null),
    dueDate: new FormControl(null),
    details: new FormControl(null),
    isPaid: new FormControl(true, Validators.required),
    type: new FormControl("property"),
    receipts: new FormControl([]),
  });
  formGroup: any;
  constructor(
    private fb: FormBuilder,
    private _propertyServices: PropertyService,
    @Inject(PLATFORM_ID) private platformId: Object,
    private _httpclint: HttpClient,
    private _expenseService: ExpenseService
  ) {
    this.expenseForm = initializeExpenseForm(fb);

    if (isPlatformBrowser(platformId)) {
      this.getAllProperties();
      this._expenseService. getAllExpenses().subscribe((resulte)=>{
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
    console.log(data.value);
    console.log(data);

    let formData = new FormData();

    formData.append("expenseType", data.get("expenseType")?.value);
    formData.append("propertyId", data.get("propertyId")?.value);
    formData.append("category", data.get("category")?.value);
    formData.append("amount", data.get("amount")?.value);
    formData.append("dueDate", data.get("dueDate")?.value);
    formData.append("details", data.get("details")?.value);
    formData.append("isPaid", data.get("isPaid")?.value);
    this.receipts.forEach(receipt => {
      formData.append('ReceiptsFiles', receipt.file, receipt.name);
    });



    let headers = new
     HttpHeaders({
      "X-OrganizationId": `${localStorage.getItem("organizationId")}`,
      Authorization: `Bearer ${localStorage.getItem("token")}`,
    });

    this._httpclint
      .post("https://localhost:7164/api/Expense/add", formData, {
        headers: headers,
      })
      .subscribe({
        next: (data) => {
          console.log(data);
        },
        error: (error) => {
          console.log(error);
        },
        complete: () => {},
      });

  }

  getAllProperties() {
    this._propertyServices.getAllProperties().subscribe({
      next: (properties) => {
        this.properties = properties.data;
      },
      error: (properties) => {},
      complete: () => {},
    });
  }

  getFieldError(field: string): string {
    const control = this.expenseForm.get(field);
    if (control?.touched && control.errors) {
      if (control.errors["required"]) {
        return `${field} is required`;
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
      const validation = validateReceipt(file); // Assume this function exists
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

    // Clear the input
    (event.target as HTMLInputElement).value = "";
  }

  removeReceipt(receipt: any): void {
    this.receipts = this.receipts.filter((r) => r.id !== receipt.id);
    // Optionally update your formGroup if it holds the receipts
    // this.formGroup.patchValue({ receipts: this.receipts });
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
