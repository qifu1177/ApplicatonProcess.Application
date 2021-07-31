export interface State {
  assetnames: string[];
  currentLN: string[];
  config: any[];
}

export const initialState: State = {
  assetnames: [],
  currentLN: ['en'],
  config: []
};

