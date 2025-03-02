import { CommonModule } from "@angular/common";
import { RouterLink, Router, ActivatedRoute } from "@angular/router";
import { ExpenseService } from "../../../../service/expense.service";
import { ExpenseFormData } from "../../../../models/expense-form.types";
import {
  FormBuilder,
  FormControl,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from "@angular/forms";
import { Expense } from "../../../../models/expense.types";
import { Company } from "../../../../models/company.types";
import { FileItemComponent } from "./file-item.component";
import { CompanyService } from "../../../../service/company.service";
import { PropertyService } from "../../../../service/property.service";
import { FileWithMetadata } from "../../../../models/fileWithMetadata.types";
import { EventEmitter, Input, Output, OnInit, Component } from "@angular/core";

@Component({
  selector: "app-expense-add-edit-page",
  standalone: true,
  imports: [RouterLink, CommonModule, ReactiveFormsModule, FileItemComponent],
  templateUrl: "./expense-add-edit.page.html",
})
export class ExpenseAddEditPage implements OnInit {
  expenseForm: FormGroup;
  @Input() expense?: Expense;
  @Output() save = new EventEmitter<ExpenseFormData>();

  files: FileWithMetadata[] = [];
  filesToDelete: string[] = [];
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
      propertyId: new FormControl(null),
      paymentSchedule: new FormControl("onetime", [Validators.required]),
      category: new FormControl(null, [Validators.required]),
      expenseType: new FormControl("property"),
      amount: new FormControl(0, [Validators.required]),
      dueDate: new FormControl(new Date().toISOString().substring(0, 10), [
        Validators.required,
      ]),
      details: new FormControl(null),
      isPaid: new FormControl(true, Validators.required),
      type: new FormControl("property"),
      CompanyId: new FormControl(null),
      files: new FormControl([]),
    });
  }

  ngOnInit(): void {
    this.getCompany();
    this.getAllProperties();

    // Check if we're editing an existing expense
    const expenseData = history.state.expense;
    if (expenseData) {
      this.loadExpenseDetails(expenseData.id);
    }
  }

  loadExpenseDetails(expenseId: string): void {
    this._expenseService.getExpenseById(expenseId).subscribe({
      next: (expense) => {
        if (expense.dueDate) {
          expense.dueDate = new Date(expense.dueDate)
            .toISOString()
            .substring(0, 10);
        }
        this.expenseForm.patchValue(expense);

        if (expense.files) {
          this.files = expense.files.map((file: any) => ({
            id: file.id,
            name: file.fileName,
            size: file.fileSize,
            type: file.fileName.endsWith(".pdf")
              ? "application/pdf"
              : "image/*",
            file: new File([], file.fileName),
            url: file.url,
          }));
          console.log("Files", this.files);
        }
      },
      error: (err) => {
        console.error("Error loading expense details:", err);
      },
    });
  }
  onFormSubmit(): void {
    if (this.expenseForm.valid) {
      const expenseData = this.expenseForm.value;

      // Format date for API
      if (expenseData.dueDate) {
        const date = new Date(expenseData.dueDate);
        if (!isNaN(date.getTime())) {
          expenseData.dueDate = date.toISOString().split("T")[0];
        }
      }

      // Prepare file files
      const files = this.files.map((file) => file.file);
      expenseData.filesToDelete = this.filesToDelete;

      // Update or create expense
      if (expenseData.id) {
        this._expenseService
          .updateExpense(expenseData.id, expenseData, files)
          .subscribe({
            next: (val: any) => {
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
      Object.keys(this.expenseForm.controls).forEach((key) => {
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

    if (this.files.length + files.length > 5) {
      this.error = "You can upload a maximum of 5 files";
      return;
    }

    this.error = ""; // Clear any previous errors

    files.forEach((fileInfo: any) => {
      const validation = validateFileWithMetadata(fileInfo);
      if (!validation.isValid) {
        this.error = validation.error || "Invalid file";
        return;
      }

      const file: FileWithMetadata = {
        id: crypto.randomUUID(),
        file: fileInfo,
        name: fileInfo.name,
        size: fileInfo.size,
        type: fileInfo.type,
      };

      this.files.push(file);
    });

    // Reset the file input
    (event.target as HTMLInputElement).value = "";
  }

  removeFile(file: any): void {
    this.files = this.files.filter((r) => r.id !== file.id);
    this.filesToDelete.push(file.id);
    this.error = "";
  }
}

function validateFileWithMetadata(file: FileWithMetadata): {
  isValid: boolean;
  error?: string;
} {
  const validTypes = ["image/jpeg", "image/png", "application/pdf"];
  const maxSize = 5 * 1024 * 1024; // 5MB

  if (!validTypes.includes(file.type)) {
    return {
      isValid: false,
      error: "Invalid file type. Please upload JPG, PNG or PDF only.",
    };
  }

  if (file.size > maxSize) {
    return {
      isValid: false,
      error: "FileWithMetadata size exceeds 5MB limit.",
    };
  }

  return { isValid: true };
}
