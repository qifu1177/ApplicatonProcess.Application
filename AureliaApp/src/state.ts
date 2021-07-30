export interface State {
  assetnames: string[];
  currentLN: string[];
}

export const initialState: State = {
  assetnames: [],
  currentLN: ['en']
};

