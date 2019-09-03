import { CoinsU } from './coinsU';

export interface User {
    id: number;
    username: string;
    coins?: CoinsU[];
}
