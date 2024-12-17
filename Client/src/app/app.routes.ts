import { Routes } from '@angular/router';
import { HomePageComponent } from './pages/home-page/home-page.component';
import { PostEditComponent } from './components/post-edit/post-edit.component';
import { postitemIdExistsGuard } from './guards/postitem-id-exists.guard';
import { PostAddComponent } from './components/post-add/post-add.component';


export const routes: Routes = [
    {path: '',component : HomePageComponent},
    {path: 'edit-post/:id', component: PostEditComponent,canActivate: [postitemIdExistsGuard] },
    { path: 'add-post', component: PostAddComponent },
    {path: '**', component: HomePageComponent},


];
