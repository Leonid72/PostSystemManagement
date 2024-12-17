import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { PostService } from '../../services/post.service';
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';
import { IPostItemDto } from '../../interfaces/IPostItemDto';
import { CommonModule } from '@angular/common';
import { PlaceService } from '../../services/place.service';
import { IPlaceDto } from '../../interfaces/IPlaceDto';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-post-add',
  standalone: true,
  imports: [CommonModule,ReactiveFormsModule],
  templateUrl: './post-add.component.html',
  styleUrl: './post-add.component.css'
})
export class PostAddComponent implements OnInit {


  postForm!: FormGroup;
  places$?: Observable<IPlaceDto[]>;
  imagePreview: string | null = null;
  selectedFile!: File;

  constructor(
    private fb: FormBuilder,
    private postService: PostService,
    private placeService: PlaceService,
    private toastr: ToastrService,
    private router: Router
  ) { }

  ngOnInit(): void {
    this.postForm = this.fb.group({
      title: ['', [Validators.required, Validators.maxLength(100)]],
      content: ['', [Validators.required]],
      placeid: [null, [Validators.required]],
      imagePath: [''],
      createdBy: ['', [Validators.required]]
    });

    this.places$ = this.placeService.getAillPlaces();
    console.log(this.places$)
  }

  onSubmit(): void {
    console.log("Form Valid:", this.postForm.valid);
    if (this.postForm.valid) {
      const formData = new FormData();
      
      // Add form data values
      formData.append('title', this.postForm.get('title')?.value);
      formData.append('content', this.postForm.get('content')?.value);
      formData.append('placeid', this.postForm.get('placeid')?.value);
      formData.append('createdBy', this.postForm.get('createdBy')?.value);
      
      // Add image file
      if (this.selectedFile) {
        formData.append('image', this.selectedFile, this.selectedFile.name);
      }
      const newPost: IPostItemDto = this.postForm.value;
      
      console.log(this.postForm.value)
      console.log(newPost)
      console.log(formData)
      this.postService.addPostItem(formData).subscribe({
        next: () => {
          this.toastr.success('Post created successfully');
          this.router.navigate(['/']);  // Redirect to the main page or list
        },
        error: (err) => {
          this.toastr.error('Failed to create post');
        }
      });
    } else {
      this.toastr.warning('Please fill in all required fields');
    }
  }


  onFileSelect(event: Event): void {
    console.log("From onFileSelect")
    const input = event.target as HTMLInputElement;
    if (input.files && input.files.length > 0) {
      this.selectedFile = input.files[0];

      // Show image preview
      const reader = new FileReader();
      reader.onload = () => {
        this.imagePreview = reader.result as string;
      };
      reader.readAsDataURL(this.selectedFile);
    }
  }

  onPlaceChange($event: Event) {
    const selectedPlaceId = ($event.target as HTMLSelectElement).value;
    console.log('Selected Place ID:', selectedPlaceId);
    this.postForm.patchValue({ placeid: selectedPlaceId });
    }
  
}




