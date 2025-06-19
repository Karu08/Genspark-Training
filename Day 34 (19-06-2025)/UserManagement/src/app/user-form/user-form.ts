import { Component, OnInit} from '@angular/core';
import { User } from '../models/user';
import { UserService } from '../services/user.service';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';

import { FormBuilder, FormGroup, Validators} from '@angular/forms';
import { CustomValidators } from '../custom-validators';

@Component({
  selector: 'app-user-form',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule ],
  templateUrl: './user-form.html',
  styleUrls: ['./user-form.css']
})

export class UserForm implements OnInit {
  userForm!: FormGroup;
  bannedWords = ['admin', 'root'];

  constructor(private fb: FormBuilder, private userService: UserService) {}

  ngOnInit(): void {
    this.userForm = this.fb.group({
      username : ['',[Validators.required, CustomValidators.bannedUsernameValidator(this.bannedWords)]],
      email : ['', [Validators.required, Validators.email]],
      password : ['', [Validators.required, CustomValidators.passwordStrengthValidator()]],
      confirmPassword : ['', Validators.required],
      role : ['User', Validators.required]
    },
    {
      validators : CustomValidators.confirmPasswordValidator('password', 'confirmPassword')
    });
  }

  onSubmit(): void {
    if (this.userForm.valid) {
      console.log('Submit button clicked')
      const { confirmPassword, ...userData } = this.userForm.value;
      this.userService.addUser(userData);
      this.userForm.reset({ role: 'User' }); 
      alert('User Added Successfully!');
    }
  }
}
