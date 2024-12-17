import { Pipe, PipeTransform } from '@angular/core';
import { IPostItemDto } from '../interfaces/IPostItemDto';

@Pipe({
  name: 'filterPost',
  standalone: true
})
export class FilterPostPipe implements PipeTransform {

  transform(posts: IPostItemDto[], search : string): IPostItemDto[] {
    if(search.length === 0 )
        return posts;
      return posts.filter(p=> p.title.toLocaleLowerCase().includes(search.toLocaleLowerCase())
                  || p.place?.toLocaleLowerCase().includes(search.toLocaleLowerCase())
    )
  }

}
