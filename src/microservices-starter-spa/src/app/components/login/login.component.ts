import { Component, inject } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { ApiClientService } from '../../services/api-client.service';
import { Router } from '@angular/router';
import { SessionService } from '../../services/session.service';


@Component({
  selector: 'app-login',
  standalone: true,
  imports: [ReactiveFormsModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.scss'
})
export class LoginComponent {
  loginForm!: FormGroup;
  private fb = inject(FormBuilder);
  private apiClient = inject(ApiClientService);
  private router = inject(Router);
  private session = inject(SessionService);

  constructor() {
   this.loginForm = this.fb.group({
    email: ['', Validators.compose([Validators.required, Validators.email])],
    password: ['', Validators.compose([Validators.required, Validators.minLength(8)])],
    confirmPassword: ['', Validators.compose([Validators.required, Validators.minLength(8)])]
   })
  }

  ngOnInit(): void {
    this.loginForm?.valueChanges.subscribe((val) => console.log(val));
  }

  login() {
    const { email, password } = this.loginForm.value;
    this.apiClient.post('auth/sign-in', {
      email: email,
      password: password,
    }).subscribe(() => {
      this.session.isLoggedIn = true;
      this.router.navigateByUrl('/dashboard');
    }, (err) => console.error(err))
  }
}
