export interface IPostItemRequestDto {
    postItemId: number;
    title: string;
    content?: string;
    // imagePath?: string;
    placeid?: string;
    createdAt: Date;
    createdBy: string;
}