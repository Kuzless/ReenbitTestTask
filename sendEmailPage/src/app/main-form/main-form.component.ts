import { Component } from '@angular/core';
import { MailService } from '../mail.service';

@Component({
  selector: 'app-main-form',
  standalone: true,
  imports: [],
  templateUrl: './main-form.component.html',
  styleUrl: './main-form.component.css'
})
export class MainFormComponent {
  emailCheck = /(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*|"(?:[\x01-\x08\x0b\x0c\x0e-\x1f\x21\x23-\x5b\x5d-\x7f]|\\[\x01-\x09\x0b\x0c\x0e-\x7f])*")@(?:(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?|\[(?:(?:(2(5[0-5]|[0-4][0-9])|1[0-9][0-9]|[1-9]?[0-9]))\.){3}(?:(2(5[0-5]|[0-4][0-9])|1[0-9][0-9]|[1-9]?[0-9])|[a-z0-9-]*[a-z0-9]:(?:[\x01-\x08\x0b\x0c\x0e-\x1f\x21-\x5a\x53-\x7f]|\\[\x01-\x09\x0b\x0c\x0e-\x7f])+)\])/;
  fileCheck = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
  constructor(private mailService: MailService) { }
  uploadData(email:string, file:FileList | null) {
    const fileList: FileList = file || new DataTransfer().files;
    if(this.validateEmail(email) && this.validateFile(fileList)) {

      const formData = new FormData();
      formData.append('email', email);
      formData.append('file', fileList[0], fileList.item(0)?.name);
      this.mailService.createItem(formData).subscribe();
      alert("Successfully uploaded! \n Check your email in 10 minutes.");
    }
  }

  validateEmail(email:string) {
    if (this.emailCheck.test(email)) {
      return true;
    }
    else {
      alert("Email is invalid.")
      return false;
    }
  }

  validateFile(file:FileList) {
    if (this.fileCheck == file.item(0)?.type) {
      return true;
    }
    else {
      alert("File type is invalid.")
      return false;
    }
  }
}