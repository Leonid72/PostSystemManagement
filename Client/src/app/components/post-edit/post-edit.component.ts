import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { FormBuilder, FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { Observable, Subject,takeUntil } from 'rxjs';
import { CommonModule } from '@angular/common';
import { PostService } from '../../services/post.service';
import { PlaceService } from '../../services/place.service';
import { IPlaceDto } from '../../interfaces/IPlaceDto';

@Component({
  selector: 'app-post-edit',
  standalone: true,
  imports: [CommonModule,ReactiveFormsModule],
  templateUrl: './post-edit.component.html',
  styleUrl: './post-edit.component.css'
})
export class PostEditComponent {

  editForm!: FormGroup;
  postId!: number;
  places$?: Observable<IPlaceDto[]>;

  private readonly destroy$ = new Subject()
  constructor(
    public fb: FormBuilder,
    private route: ActivatedRoute,
    private postService: PostService,
    private placeService: PlaceService,
    public router: Router,
    private toastr: ToastrService
  ) {}

  ngOnInit(): void {
    // Get postId from route parameters
    console.log("Oninit")
    this.postId = +this.route.snapshot.paramMap.get('id')!;
    
    // Create reactive form
    this.editForm = this.fb.group({
      postItemId:  [this.postId],
      title: ['', [Validators.required, Validators.maxLength(50)]],
      content: ['', [Validators.required]],
      placeid: [null, [Validators.required]],
      createdBy: ['', Validators.required],
      imagePath: ['']
    });

    // Load existing post details
    this.loadPostData();

    this.places$ = this.placeService.getAillPlaces();
    console.log(this.places$)
  }

  loadPostData(): void {
    this.postService.getPostItemById(this.postId).pipe(
      takeUntil(this.destroy$)

    ).subscribe(post => {
      this.editForm.patchValue({
        title: post.title,
        content: post.content,
        place: post.place,
        imagePath: post.imagePath,
        createdBy: post.createdBy
      });
    });
    console.log("Load Data")
    console.log('Form value after patch:', this.editForm.value)
  }

  onSubmit(): void {
    if (this.editForm.valid) {
      this.postService.updatePostItem(this.postId, this.editForm.value).subscribe({
        next: () => {
          this.toastr.success('Post updated successfully!');
          this.router.navigate(['/posts']); // Navigate back to posts page
        },
        error: (err) => {
          console.error('Error updating post:', err);
          this.toastr.error('Failed to update the post.');
        }
      });
    } else {
      this.toastr.info('Please fill in all required fields.');
    }
  }

  onPlaceChange($event: Event) {
    const selectedPlaceId = ($event.target as HTMLSelectElement).value;
    console.log('Selected Place ID:', selectedPlaceId);
    this.editForm.patchValue({ placeid: selectedPlaceId });
    }

     // Unsubscribe from stream
     ngOnDestroy(): void {
      this.destroy$.next(true);
      this.destroy$.complete();
    }
}
