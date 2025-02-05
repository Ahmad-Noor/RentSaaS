export interface FeatureTab {
  id: string;
  label: string;
  content: {
    title: string;
    description: string;
    imageUrl: string;
  };
}

export interface TabState {
  activeTabId: string;
}