package com.example.demo;

import jakarta.persistence.*;
//Chaque Order peut avoir un ou plusieurs Payments associés
@Entity
@Table(name = "orders")
public class Order {

    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private Long orderId;

    private String reservationId;
    private String status;//Created,Confirmed",In Preparation,Ready
    private String menuItemId; 
    private int quantity; 

    public Order() {}

    public Order(String reservationId, String status, String menuItemId, int quantity) {
        this.reservationId = reservationId;
        this.status = status;
        this.menuItemId = menuItemId;
        this.quantity = quantity;
    }

   
    public Long getOrderId() { return orderId; }
    public String getReservationId() { return reservationId; }
    public String getStatus() { return status; }
    public String getMenuItemId() { return menuItemId; }
    public int getQuantity() { return quantity; }

    public void setReservationId(String reservationId) { this.reservationId = reservationId; }
    public void setStatus(String status) { this.status = status; }
    public void setMenuItemId(String menuItemId) { this.menuItemId = menuItemId; }
    public void setQuantity(int quantity) { this.quantity = quantity; }
}
