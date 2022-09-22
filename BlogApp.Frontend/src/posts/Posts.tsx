import React, { FC, ReactElement, useState, useRef, useEffect } from "react";
import { CreatePostDto, Client, GetPostsDto } from "../api/api-main";
import { FormControl } from 'react-bootstrap';

const apiClientMain = new Client('https://localhost:7163/');
const apiClientAuth = new Client('https://localhost:7090/');

async function createPost(post: CreatePostDto) {
    await apiClientMain.createPost('1.0', post);
    console.log('Post is created.');
}

const Posts: FC<{}> = (): ReactElement => {
    let textInput = useRef(null);
    const [posts, setPosts] = useState<GetPostsDto[] | undefined>(undefined);

    async function getPosts() {
        const postList = await apiClientMain.getAllPosts('1.0');
        setPosts(postList);
    }

    useEffect(() => {
        setTimeout(getPosts, 500);
    }, []);

    const handleKeyPress = (event: React.KeyboardEvent<HTMLInputElement>) => {
        if (event.key === 'Enter') {
            const post: CreatePostDto = {
                header: event.currentTarget.value,
            };
            createPost(post);
            event.currentTarget.value = '';
            setTimeout(getPosts, 500);
        }
    };

    return (
        <div>
            Notes
            <div>
                <FormControl ref={textInput} onKeyPress={handleKeyPress} />
            </div>
            <section>
                {posts?.map((post) => (
                    <div>{post.text}</div>
                ))}
            </section>
        </div>
    );
};
export default Posts;