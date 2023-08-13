import { User } from "oidc-client-ts"

type UserState = {
    user: User | null
}

type UserReducerAction = {
    type: 'set' | 'unset',
    payload: User | null
}

const userReducer = (state: UserState, action: UserReducerAction): UserState => {
    switch (action.type){
        case 'set':
            return { user: action.payload }
        default: throw new Error();
    }
}

export default userReducer;