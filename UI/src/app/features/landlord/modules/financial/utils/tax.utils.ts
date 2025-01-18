export function formatTaxAmount(amount: number): string {
  return new Intl.NumberFormat('en-US', {
    style: 'currency',
    currency: 'USD'
  }).format(amount);
}

export function getStatusClass(status: string): string {
  const baseClasses = 'px-2 py-1 rounded-full text-sm';
  const statusClasses: Record<string, string> = {
    'paid': 'bg-green-100 text-green-800',
    'pending': 'bg-yellow-100 text-yellow-800',
    'overdue': 'bg-red-100 text-red-800'
  };

  return `${baseClasses} ${statusClasses[status] || 'bg-gray-100 text-gray-800'}`;
}