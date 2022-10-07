import { CoinsHodle } from './coinsHodle';

export interface Portfolio {
    portfolioID: string;
    portfolioName: string;
    userid: number;
    coinsHodle?: CoinsHodle[];
}
