import { Component, OnInit, OnDestroy } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { User } from '../_models';
import { UserService, AuthenticationService } from '../_services';
import { PasswordValidator, EmailValidator } from '../_validators';
import { first } from 'rxjs/operators';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})

export class HomeComponent implements OnInit {
  public user: User;
  registerForm: FormGroup;
  loading = false;
  submitted = false;
  addForm = false;
  showRegistration = false;
  isAutenticated = false;
  error = '';
  message = '';


  constructor(
    private formBuilder: FormBuilder,
    private route: ActivatedRoute,
    private router: Router,
    private userService: UserService,
    private authService: AuthenticationService,
  ) { }

  get f() { return this.registerForm.controls; }


  ngOnInit(): void {
    this.registerForm = this.formBuilder.group({
      password: ['', Validators.required],
      fullName: ['', Validators.required],  
      confirmPassword: [
        '',
        [
          Validators.required,
          PasswordValidator('password'),
          Validators.maxLength(50)
        ]
      ],
      username: [
        '',
        [Validators.required, Validators.email, Validators.maxLength(50)]
      ]
    });
    this.isAutenticated = this.authService.isLoggedIn();
  }
  showReg() {
    this.showRegistration = true;
  }
  hideReg() {
    this.showRegistration = false;
  }
  onSubmit(): void {
    this.submitted = true;
    this.loading = true;
    if (this.registerForm.invalid) {
      return;
    }
    this.userService.register(this.f.username.value, this.f.username.value, this.f.fullName.value,this.f.password.value)
      .pipe(first())
      .subscribe(
        data => {
          this.message = data.message;
          this.loading = false;
          this.registerForm.reset();
          this.router.navigate(['/my-recipes']);
        },
        error => {
          console.log(error);
          this.error = error;
          this.loading = false;
        });


    setTimeout(() => {
      this.message = '';
    }, 3000)
  }


}
