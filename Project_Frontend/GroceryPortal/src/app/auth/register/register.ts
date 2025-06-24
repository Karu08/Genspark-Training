import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import {Router} from '@angular/router';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [ReactiveFormsModule, CommonModule],
  templateUrl: './register.html',
  styleUrls: ['../login/login.css']
})

export class Register {
  registerForm: FormGroup;
  successMsg = '';
  errorMsg = '';

  constructor(private fb: FormBuilder, private auth: AuthService, private router: Router)
  {
    this.registerForm  = this.fb.group({
      username: ['',Validators.required],
      password: ['',Validators.required],
      role: ['',Validators.required],
    });
  }

  onSubmit() {
    if (this.registerForm.invalid) return;

    this.auth.register(this.registerForm.value).subscribe({
      next: () => {
        this.successMsg = 'Registered successfully!';
        this.router.navigate(['/login']);
      },
      error: () => {
        this.errorMsg = 'Something went wrong. Please try again!';
      }
    });
  }
}
