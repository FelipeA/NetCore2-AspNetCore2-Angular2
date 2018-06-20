import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Validators, FormBuilder, FormGroup } from '@angular/forms';
import { CustomValidator } from '../../validators/custom.validator';
import { DataService } from '../../services/data.service';
import { UI } from '../../utils/UI';

@Component({
  selector: 'app-signup-page',
  templateUrl: './signup-page.component.html',
  providers: [UI, DataService]
})
export class SignupPageComponent implements OnInit {
  public form: FormGroup;
  public errors: any[] = [];

  constructor(private fb: FormBuilder, private ui: UI, private dataService: DataService, private router: Router) {
    this.form = this.fb.group({
      firstName: ['Felipe', Validators.compose([
        Validators.minLength(3),
        Validators.maxLength(40),
        Validators.required
      ])],
      lastName: ['Augusto', Validators.compose([
        Validators.minLength(3),
        Validators.maxLength(40),
        Validators.required
      ])],
      email: ['flp.augusto@gmail.com', Validators.compose([
        Validators.minLength(5),
        Validators.maxLength(160),
        Validators.required,
        CustomValidator.EmailValidator
      ])],
      document: ['33767140888', Validators.compose([
        Validators.minLength(11),
        Validators.maxLength(11),
        Validators.required
      ])],
      username: ['felipeaugusto', Validators.compose([
        Validators.minLength(6),
        Validators.maxLength(20),
        Validators.required
      ])],
      password: ['123456', Validators.compose([
        Validators.minLength(6),
        Validators.maxLength(20),
        Validators.required
      ])],
      confirmPassword: ['123456', Validators.compose([
        Validators.minLength(6),
        Validators.maxLength(20),
        Validators.required
      ])]
    });

   }

  ngOnInit() {
  }

  checkEmail() {
    this.ui.lock('emailControl');
    // document.getElementById('emailControl').classList.add('is-loading');
    this.form.controls['email'].disable();
    setTimeout(() => {
      console.log(this.form.controls['email'].value);

      this.ui.unlock('emailControl');
      this.form.controls['email'].enable();
      // document.getElementById('emailControl').classList.remove('is-loading');
    }, 3000);
  }

  submit()
  {
    this.dataService.createuser(this.form.value)
    .subscribe(result => {
      alert('Bem vindo ao site!');

      this.router.navigateByUrl('/');
    }, error => {
      var data = JSON.parse(error._body);
      //alert(data.errors[0].message);
      this.errors = data.errors;
    });
  }
}
