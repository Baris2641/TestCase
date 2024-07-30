import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AuthService } from '../auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent {
  registerForm: FormGroup;

  constructor(
    private fb: FormBuilder,
    private authService: AuthService,
    private router: Router
  ) {
    this.registerForm = this.fb.group({
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      phone: ['', Validators.required],
      password: ['', Validators.required],
      profilePicture: [null]
    });
  }

  onFileChange(event: any) {
    if (event.target.files.length > 0) {
      const file = event.target.files[0];
      const profilePictureControl = this.registerForm.get('profilePicture');
      if (profilePictureControl) {
        profilePictureControl.setValue(file);
      }
    }
  }

  submit() {
    const formData: any = new FormData();
    const firstNameControl = this.registerForm.get('firstName');
    const lastNameControl = this.registerForm.get('lastName');
    const emailControl = this.registerForm.get('email');
    const phoneControl = this.registerForm.get('phone');
    const passwordControl = this.registerForm.get('password');
    const profilePictureControl = this.registerForm.get('profilePicture');

    if (firstNameControl) {
      formData.append('firstName', firstNameControl.value);
    }
    if (lastNameControl) {
      formData.append('lastName', lastNameControl.value);
    }
    if (emailControl) {
      formData.append('email', emailControl.value);
    }
    if (phoneControl) {
      formData.append('phone', phoneControl.value);
    }
    if (passwordControl) {
      formData.append('password', passwordControl.value);
    }
    if (profilePictureControl && profilePictureControl.value) {
      formData.append('profilePicture', profilePictureControl.value);
    }

    this.authService.register(formData).subscribe(
      response => {
        console.log('Registration successful', response);
        this.router.navigate(['/login']);  // Burada Login ekranına yönlendirme işlemini sağlıyoruz
      },
      error => {
        console.error('Registration failed', error);
      }
    );
  }
}
