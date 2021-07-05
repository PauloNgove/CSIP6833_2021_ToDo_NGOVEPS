import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { AccountService } from '../_services/account.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  @Output() cancelRegister = new EventEmitter();
  model: any = {};
  registerForm: FormGroup =  new FormGroup({
    username: new FormControl(),
    password: new FormControl(),
    confirmPassword: new FormControl()
  });
  registerMode = true;
  constructor(private accountService: AccountService) { }

  ngOnInit(): void {
  }
  initializeForm() {
    this.registerForm = new FormGroup({
      username: new FormControl(),
      password: new FormControl(),
      confirmPassword: new FormControl()
    });
    // this.registerForm.controls.password.valueChanges.subscribe(() => {
    //   this.registerForm.controls.confirmPassword.updateValueAndValidity();
    // });
  }
  
register()
{
  this.accountService.register(this.model).subscribe(response =>{
  console.log(response);
  this.cancel();
  }, error => {
    console.log(error);
  })
  console.log(this.registerForm.value);
}
cancel()
{
  this.registerMode = false;
}
}
