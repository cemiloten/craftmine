extern crate amethyst;
use amethyst::core::{
    shrev::{EventChannel, ReaderId},
    specs::{Read, Resources, SystemData},
    EventReader,
};
use amethyst::ecs::{Component, DenseVecStorage};
use amethyst::input::is_key_down;
use amethyst::prelude::*;
use amethyst::renderer::VirtualKeyCode;

enum Shape {
    Sphere { radius: f32 },
    RectangularPrism { height: f32, width: f32, depth: f32 },
}

pub struct Content {
    content_name: String,
}

impl Component for Shape {
    type Storage = DenseVecStorage<Self>;
}

impl Component for Content {
    type Storage = DenseVecStorage<Self>;
}

fn largest<T: PartialOrd + Copy>(list: &[T]) -> T {
    let mut largest = list[0];
    for &item in list.iter() {
        if item > largest {
            largest = item
        }
    }

    largest
}

// fn largest2<T: PartialOrd + Clone>(list: &[T]) -> T {
//     let mut largest = list[0].clone();
//     for &item in list.iter() {
//         if item > largest {
//             largest = item
//         }
//     }

//     largest
// }

fn largest3<T: PartialOrd>(list: &[T]) -> &T {
    let mut largest = &list[0];
    for item in list.iter() {
        if item > largest {
            largest = item
        }
    }

    largest
}

struct Point<T, U> {
    x: T,
    y: U,
}

impl<T, U> Point<T, U> {
    fn mixup<V, W>(self, other: Point<V, W>) -> Point<T, W> {
        Point {
            x: self.x,
            y: other.y,
        }
    }
}

pub trait Summary {
    fn summarize_author(&self) -> String;

    fn summarize(&self) -> String {
        format!("(Read more from {}...)", self.summarize_author())
    }
}

pub struct NewsArticle {
    pub headline: String,
    pub location: String,
    pub author: String,
    pub content: String,
}

impl Summary for NewsArticle {
    fn summarize_author(&self) -> String {
        format!("{} writes well", self.author)
    }

    fn summarize(&self) -> String {
        format!("{}, by {} ({})", self.headline, self.author, self.location)
    }
}

pub struct Tweet {
    pub username: String,
    pub content: String,
    pub reply: bool,
    pub retweet: bool,
}

impl Summary for Tweet {
    fn summarize_author(&self) -> String {
        format!("@{}", self.username)
    }

    fn summarize(&self) -> String {
        format!("{}: {}", self.username, self.content)
    }
}

pub fn notify(item: impl Summary) {
    println!("Breaking news! {}", item.summarize());
}

pub fn notify_two<T: Summary>(item1: T, item2: T) {
    println!("Breaking news! {}", item1.summarize());
    println!("Breaking news! {}", item2.summarize());
}

fn main() {
    let number_list = vec![34, 50, 25, 100, 64];
    println!("lajrkl : {}", largest(&number_list));
    let number_list2 = vec![34., 50.0f32, 25.5, 100.5, 64.];
    println!("lajrkl : {}", largest(&number_list2));

    let p1 = Point { x: 5, y: 5.0 };
    let p2 = Point { x: "hi", y: 'c' };
    let _p3 = p1.mixup(p2);
}
