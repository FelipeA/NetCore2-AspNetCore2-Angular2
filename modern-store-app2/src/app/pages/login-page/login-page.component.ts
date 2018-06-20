import { Component, OnInit } from '@angular/core';
import { Validators, FormBuilder, FormGroup } from '@angular/forms';
import { CustomValidator } from '../../validators/custom.validator';
import { Router } from '@angular/router';
import { UI } from '../../utils/UI';
import { DataService } from '../../services/data.service';

@Component({
  selector: 'app-login-page',
  templateUrl: './login-page.component.html',
  providers: [UI, DataService]
})
export class LoginPageComponent implements OnInit {

  public form: FormGroup;
  private errors: any[] = [];

  constructor(private fb: FormBuilder, private ui: UI, private dataService: DataService, private router: Router) {
    this.form = this.fb.group({
      username: ['', Validators.compose([
        Validators.minLength(5),
        Validators.maxLength(160),
        Validators.required
      ])],
      password: ['', Validators.compose([
        Validators.minLength(6),
        Validators.maxLength(20),
        Validators.required
      ])]
    });

    this.checkToken();
  }

  submit() {
    this.dataService.authenticate(this.form.value)
      .subscribe(result => {
        localStorage.setItem('mws.token', result.token);
        localStorage.setItem('mws.user', JSON.stringify(result.user));

        this.router.navigateByUrl('/home');
      }, error => {
        this.errors = JSON.parse(error._body).errors;
      });
  }

  checkToken() {
    var token = localStorage.getItem('mws.token');

    if (token) {
      if (this.dataService.validateToken(token)) {
        this.router.navigateByUrl('/home');
      }
    }

  }

  showModal() {
    this.ui.setActive('modal')
  }

  hideModal() {
    this.ui.setInactive('modal')
  }

  ngOnInit() {
  }
}
