import { Component, OnDestroy, OnInit } from '@angular/core';
import { Observable, Subject, takeUntil } from 'rxjs';
import { IPostItemDto } from '../../interfaces/IPostItemDto';
import { PostService } from '../../services/post.service';
import { CommonModule } from '@angular/common';
import { ToastrService } from 'ngx-toastr';
import { Router, RouterModule } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { FilterPostPipe } from "../../pipes/filter-post.pipe";


@Component({
  selector: 'app-post-page',
  standalone: true,
  imports: [CommonModule, RouterModule, FormsModule, FilterPostPipe],
  templateUrl: './post-page.component.html',
  styleUrl: './post-page.component.css'
})
export class PostPageComponent implements OnInit ,OnDestroy{

searchQuery: string = "";

  constructor(public postService : PostService,private toastr: ToastrService,private router: Router){}
  posts$?: Observable<IPostItemDto[]>;
  private readonly destroy$ = new Subject()

  ngOnInit(): void {
    this.posts$ = this.postService.getAillPosts();
  }

  onDelete(postItemId: number) {
    if (confirm('Are you sure you want to delete this post?')) {
      this.postService.deletePostItem(postItemId)
        .pipe(
          takeUntil(this.destroy$)
        )
        .subscribe({
          next: () => {
            this.toastr.success("Task deleted successfully");
            this.posts$ =  this.postService.getAillPosts(); // Refresh the list
          },
          error: (err) => {
            this.toastr.error("Faield to delete");
          }
      });
    }
  }
  onEdit(postId: number): void {
    this.router.navigate(['/edit-post', postId]).then(() => {
      console.log('Navigated to edit-post page');
    }).catch(err => {
      console.error('Navigation error:', err);
    });
  }
  
      // Unsubscribe from stream
      ngOnDestroy(): void {
        this.destroy$.next(true);
        this.destroy$.complete();
      }
}
