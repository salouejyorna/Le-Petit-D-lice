package com.example.demo;

import java.util.List;
import java.util.Optional;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;
import org.springframework.transaction.annotation.Transactional;

@Service
public class OrderService {

    @Autowired
    private OrderRepository orderRepository;

    @Transactional
    public Order createOrder(String reservationId, String menuItemId, int quantity) {
        Order order = new Order(reservationId, "Created", menuItemId, quantity);
        return orderRepository.save(order);
    }

    public Order getOrderById(Long orderId) {
         Optional<Order> orderOpt = orderRepository.findById(orderId);
        return orderOpt.orElse(null); 
    }
    
    public List<Order> getAllOrders() {
        return orderRepository.findAll();
    }
}
