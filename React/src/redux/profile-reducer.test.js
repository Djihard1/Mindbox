import profileReducer, {addPostActionCreator, deletePost} from "./profile-reducer";
import ReactDOM from "react-dom";
import App from "../App";


let state = {
    posts: [
        {id: 1, message: 'Hi, how are you?', likesCount: 12},
        {id: 2, message: 'It\'s my first post', likesCount: 11},
        {id: 3, message: 'Blabla', likesCount: 11},
        {id: 4, message: 'Dada', likesCount: 11}
    ]};






it('new Post should be added', () => {
    let action = addPostActionCreator('Vasia!!! YI!!')
    let newState = profileReducer(state, action)
    expect(newState.posts.length).toBe(5)

});

it('message should be Vasia', () => {
    let action = addPostActionCreator('Vasia!!! YI!!')
    let newState = profileReducer(state, action)

    expect(newState.posts[4].message).toBe("Vasia!!! YI!!")
});

it('after deleting lengh messages should be decrement', () => {
    let action = deletePost(1)
    let newState = profileReducer(state, action)
    expect(newState.posts.length).toBe(3)

});

