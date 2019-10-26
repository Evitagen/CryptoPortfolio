import { Portfolio } from './portfolio';

export interface User {
    id: number;
    username: string;
    price: number;
    portfolio?: Portfolio[];
}
