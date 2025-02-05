export interface SidebarState {
  isCollapsed: boolean;
}

export interface SidebarProps {
  isCollapsed: boolean;
  onCollapsedChange: (collapsed: boolean) => void;
}