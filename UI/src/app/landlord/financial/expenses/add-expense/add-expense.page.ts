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
  // styleUrls: ["./add-expense.page.css"],
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
    private expenseService: ExpenseService,
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
      amount: new FormControl(100, [Validators.required]),
      dueDate: new FormControl(null, [Validators.required]),
      details: new FormControl(null),
      isPaid: new FormControl(true, Validators.required),
      type: new FormControl("property"),
      CompanyId: new FormControl(null),
      receipts: new FormControl([]),
    });
  }
  ngOnInit() {
    //this.expenseForm.patchValue(this.data);
    this.getCompany();
  }

  onFormSubmit(): void {
    if (this.expenseForm.valid) {
      // if (this.data) {
      //   this._expenseService
      //     .updateExpense(this.data.id, this.expenseForm.value)
      //     .subscribe({
      //       next: (val: any) => {
      //         // this._coreService.openSnackBar('Address detail updated!');
      //         console.log('Address detail updated!');
      //       },
      //       error: (err: any) => {
      //         console.error(err);
      //       },
      //     });
      // } else {
      this._expenseService
        .addExpense(this.expenseForm.value as Expense)
        .subscribe({
          next: (val: any) => {
            // this._coreService.openSnackBar('Expense added successfully');
            console.log("Expense added successfully");
          },
          error: (err: any) => {
            console.error(err);
          },
        });

      // };
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
