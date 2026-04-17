import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { Router, RouterOutlet } from '@angular/router';
import { RouterModule } from '@angular/router';
import { HasPermissionDirective } from '../../directives/has-permission.directive';
import { AuthService } from '../../../services/auth.service';

@Component({
  selector: 'app-main-layout',
  standalone: true,
  imports: [RouterModule, CommonModule,HasPermissionDirective],
    templateUrl: './main-layout.component.html',
  styleUrl: './main-layout.component.css'
})
export class MainLayoutComponent {
userName: string | null = null;
branchName: string | null = null;
roles: string[] = [];

constructor(private auth: AuthService ,private  router: Router) {}

ngOnInit() {
this.userName = this.auth.getUserName();
const rawBranch = this.auth.getBranchName();
this.branchName = rawBranch ? decodeURIComponent(escape(rawBranch)) : null;

  this.roles = this.auth.getRoles(); // خليه زي ما هو
}


   currentYear = new Date().getFullYear();
     
   isSidebarOpen = false;

  toggleSidebar() {
    this.isSidebarOpen = !this.isSidebarOpen;
  }
   closeSidebar() {
    this.isSidebarOpen = false;
  }
logout(){
    this.auth.logout();
  this.router.navigate(['/login']);
}
}
