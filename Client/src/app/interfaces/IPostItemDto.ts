export interface IPostItemDto {
    postItemId: number;
    title: string;
    content?: string;
    imagePath?: string;
    place?: string;
    createdAt: Date;
    createdBy: string;
}