import { Component } from '@angular/core';
import { AuthService } from '../../../dashboard/services/user/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent {

  // loginObj: any = {
  //   "userId": 0,
  //   "emailId": "",
  //   "fullName": "string",
  //   "password": ""
  // }
  emailId: any = "";
  password: any = "";


  constructor(private auth:AuthService ) {

  }

  onLogin() {
    // this.http.post("https://freeapi.miniprojectideas.com/api/Jira/Login",this.loginObj).subscribe((res:any)=>{
    //   debugger;
    //   if(res.data) {
    //     localStorage.setItem('rentSaaSLogin', JSON.stringify(res.data));
    //     this.router.navigateByUrl('/dashboard');
    //   } else {
    //     alert(res.message)
    //   }
    // }
       //)

       return this.auth.login({
        emailId: this.emailId,
        password: this.password,
       })
  }

}