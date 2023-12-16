import { Component, inject } from '@angular/core';
import { Router } from '@angular/router';
import { SessionService } from '../../services/session.service';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [],
  templateUrl: './dashboard.component.html',
  styleUrl: './dashboard.component.scss'
})
export class DashboardComponent {
  private router = inject(Router);
  private session = inject(SessionService);

  logout() {
    this.session.isLoggedIn = false;
    this.router.navigateByUrl("/");
  }
}
