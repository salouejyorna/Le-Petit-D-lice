package com.example.demo;

import jakarta.ws.rs.*;
import jakarta.ws.rs.core.MediaType;
import jakarta.ws.rs.core.Response;

import java.util.List;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Component;

@Component
@Path("/order-service")
public class Service {

    @Autowired
    private OrderService orderService;

    @POST
    @Path("/orders")
    @Consumes(MediaType.APPLICATION_JSON)
    @Produces(MediaType.APPLICATION_JSON)
    public Response createOrder(Order orderRequest) {

    	Order createdOrder = orderService.createOrder(
                orderRequest.getReservationId(),
                orderRequest.getMenuItemId(),
                orderRequest.getQuantity()
        );
        return Response.ok(createdOrder).build();
    }

    @GET
    @Path("/orders")
    @Produces(MediaType.APPLICATION_JSON)
    public Response getAllOrders() {
        List<Order> orders = orderService.getAllOrders();
        if (orders != null && !orders.isEmpty()) {
            return Response.ok(orders).build();
        } else {
            return Response.status(Response.Status.NO_CONTENT).build(); // 204 si la liste est vide
        }
    }

    @GET
    @Path("/orders/{orderId}")
    @Produces(MediaType.APPLICATION_JSON)
    public Response getOrderById(@PathParam("orderId") Long orderId) {
        Order order = orderService.getOrderById(orderId);
        if (order != null) {
            return Response.ok(order).build();
        } else {
            return Response.status(Response.Status.NOT_FOUND).build();
        }
    }
}
