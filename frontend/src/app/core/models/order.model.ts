export type OrderStatus = "pending" | "paid" | "shipped" | "cancelled";

export interface Order {
  id: number;
  customerId: number;
  totalAmount: number;
  status: OrderStatus;
  createdAt: Date;
}