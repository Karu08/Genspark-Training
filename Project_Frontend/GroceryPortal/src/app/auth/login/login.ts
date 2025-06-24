import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import {Router} from '@angular/router';
import { CommonModule } from '@angular/common';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [ReactiveFormsModule, CommonModule],
  templateUrl: './login.html',
  styleUrls: ['./login.css']
})

export class Login {
  loginForm: FormGroup;
  errorMsg= '';

  constructor(private fb: FormBuilder, private auth: AuthService, private router: Router)
  {
    this.loginForm  = this.fb.group({
      username: ['',Validators.required],
      password: ['',Validators.required],
    });
  }

  onSubmit() {
    if (this.loginForm .invalid) return;
    const {username, password} = this.loginForm.value;

    this.auth.login(username, password).subscribe({
      next: (res) => {
        this.auth.storeToken(res.token);
        this.router.navigate([`/${res.role.toLowerCase()}/home`]);
      },
      error: () => {
        this.errorMsg = 'Invalid Credentials';
      }
    });
  }
}
