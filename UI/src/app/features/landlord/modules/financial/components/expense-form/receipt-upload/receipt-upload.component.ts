import { Component, Input } from "@angular/core";
import { CommonModule } from "@angular/common";
import { FormGroup, ReactiveFormsModule } from "@angular/forms";
import { ReceiptItemComponent } from "./receipt-item.component";
import { Receipt } from "../../../types/receipt.types";
import { validateReceipt } from "../../../utils/receipt.utils";

@Component({
  selector: "app-receipt-upload",
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, ReceiptItemComponent],
  templateUrl: "./receipt-upload.component.html",
})
export class ReceiptUploadComponent {
  @Input() formGroup!: FormGroup;



  // receipts: Receipt[] = [];
  // error = "";

  // onFilesSelected(event: Event): void {
  //   const files = Array.from((event.target as HTMLInputElement).files || []);

  //   if (this.receipts.length + files.length > 5) {
  //     this.error = "You can upload a maximum of 5 receipts";
  //     return;
  //   }

  //   files.forEach((file) => {
  //     const validation = validateReceipt(file);
  //     if (!validation.isValid) {
  //       this.error = validation.error || "Invalid file";
  //       return;
  //     }

  //     const receipt: Receipt = {
  //       id: crypto.randomUUID(),
  //       file,
  //       name: file.name,
  //       size: file.size,
  //       type: file.type,
  //     };

  //     this.receipts.push(receipt);
  //   });

  //   this.formGroup.patchValue({ receipts: this.receipts });
  //   (event.target as HTMLInputElement).value = "";
  // }

  // removeReceipt(receipt: Receipt): void {
  //   this.receipts = this.receipts.filter((r) => r.id !== receipt.id);
  //   this.formGroup.patchValue({ receipts: this.receipts });
  //   this.error = "";
  // }


  
}
