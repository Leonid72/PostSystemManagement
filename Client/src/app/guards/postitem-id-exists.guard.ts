import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { PostService } from '../services/post.service';
import { catchError, map, of } from 'rxjs';

export const postitemIdExistsGuard: CanActivateFn = (route, state) => {

  const router = inject(Router)
  const postItemServer = inject(PostService);
  const toastr =  inject(ToastrService);
  const id = route.paramMap.get('id');

  if (Number(id) <= 0) {
    toastr.error(`TaskId ${id} not Valid`);
    router.navigate(['/not-found']);
    return of(false);
  }

  return postItemServer.getPostItemById(Number(id)).pipe(
    map(postItem => {
      if (postItem) {
        return true;
      } else {
        router.navigate(['/']);
        return false;
      }
    }),
    catchError(() => {
      toastr.error(`PostItemId ${id} not Valid`);
      router.navigate(['/']);
      return of(false);
    })
  );

};
