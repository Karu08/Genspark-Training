import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import {Router} from '@angular/router';
import { CommonModule } from '@angular/common';
import { AuthService } from '../../services/auth.service';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [ReactiveFormsModule, CommonModule, RouterModule],
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
    // Store token
    this.auth.storeToken(res.accessToken);

    // Decode token to extract role
    const decodedToken: any = this.auth.decodeToken(res.accessToken);
    const role = decodedToken.role?.toLowerCase();

    // Redirect to role-based home page
    if (role) {
      this.router.navigate([`/${role}/home`]);
    } else {
      this.errorMsg = 'Unable to determine user role.';
    }
  },
  error: () => {
    this.errorMsg = 'Invalid Credentials';
  }
});


  }
}
