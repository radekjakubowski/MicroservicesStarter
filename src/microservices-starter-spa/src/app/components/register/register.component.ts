import { Component, OnInit, inject } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { ApiClientService } from '../../services/api-client.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [ReactiveFormsModule],
  templateUrl: './register.component.html',
  styleUrl: './register.component.scss'
})
export class RegisterComponent implements OnInit {
  registerForm!: FormGroup;
  private fb = inject(FormBuilder);
  private apiClient = inject(ApiClientService);
  private router = inject(Router);

  constructor() {
   this.registerForm = this.fb.group({
    email: ['', Validators.compose([Validators.required, Validators.email])],
    password: ['', Validators.compose([Validators.required, Validators.minLength(8)])],
    confirmPassword: ['', Validators.compose([Validators.required, Validators.minLength(8)])]
   })
  }

  ngOnInit(): void {
    this.registerForm?.valueChanges.subscribe((val) => console.log(val));
  }

  register() {
    const { email, password, confirmPassword } = this.registerForm.value;
    this.apiClient.post('auth/sign-up', {
      email: email,
      password: password,
      confirmPassword: confirmPassword
    }).subscribe(() => this.router.navigateByUrl('/'), (err) => console.error(err))
  }
}
