import { Component, Inject, OnInit } from '@angular/core';
import { AuthenticationService } from '../_services';
import { Router, ActivatedRoute } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Observable } from 'rxjs';
@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent implements OnInit {
  constructor(
    private authenticationService: AuthenticationService,
  ) {}

  ngOnInit(): void {
    this.isLoggedIn = this.authenticationService.isLoggedIn();
  }
  isExpanded = false;
  isLoggedIn = false;

  collapse() {
    this.isExpanded = false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }
}
