import matplotlib as mpl
import matplotlib.pyplot as plt
import matplotlib.patches as patches
import random
import numpy as np

def is_inside_plot(x, y):
    return x >= 0 and x <= 0.9 and y >= 0 and y <= 0.9

def add_rect(ax, x, y, width=0.1, height=0.1, color='red', angle=None):
    rect = patches.Rectangle((x, y), width, height, linewidth=1, edgecolor=color, facecolor='none')
    if angle is not None:
        center_x = x + width / 2
        center_y = y + height / 2
        t = mpl.transforms.Affine2D().rotate_around(center_x, center_y, angle) + ax.transData
        rect.set_transform(t)
    ax.add_patch(rect)

def do_circles_intersect(c1, c2):
    # c1 and c2 are tuples of (x, y, r)
    return (c1[0] - c2[0])**2 + (c1[1] - c2[1])**2 < (c1[2] + c2[2])**2

def randomly(ax, num_images=10):
    for image in range(num_images):
        x = random.random()
        y = random.random()
        add_rect(ax, x, y, color='red')

def randomly_smart(ax, num_images=10):
    prev_img = []
    for image in range(num_images):
        x = -1;
        y = -1;
        valid = False
        while not valid:
            x = random.random()
            y = random.random()
            violated = False
            for (x_old, y_old) in prev_img:
                if abs(x - x_old) < 0.11 and abs(y - y_old) < 0.11 or not is_inside_plot(x, y):
                    violated = True
                    break
            if not violated:
                valid = True
        add_rect(ax, x, y, color='red')
        prev_img.append((x, y))

def grid(ax, num_images=9):
    for image in range(num_images):
        nx, ny = (3, 3)
        x = np.linspace(0.25, 0.65, nx)
        y = np.linspace(0.25, 0.65, ny)
        xv, yv = np.meshgrid(x, y)
        for i in range(nx):
            for j in range(ny):
                add_rect(ax, xv[i][j], yv[i][j], color='red')   

def sun(ax, num_images=25, radius=0.5):
    # place image in a circle
    prev_img = []
    for image in range(num_images):
        r = radius

        x = -1;
        y = -1;
        valid = False
        while not valid:
            angle = random.random() * 2 * np.pi
            x = r * np.cos(angle) + 0.5
            y = r * np.sin(angle) + 0.5
            violated = False
            for (x_old, y_old) in prev_img:
                if do_circles_intersect((x+0.1/2, y+0.1/2, 0.0707), (x_old+0.1/2, y_old+0.1/2, 0.0707)) or not is_inside_plot(x, y):
                    violated = True
                    break
            if not violated:
                valid = True
        add_rect(ax, x, y, color='red', angle=angle)
        prev_img.append((x, y))

def main():
    fig, ax = plt.subplots()

    sun(ax, 7, 0.55)
    sun(ax, 10, 0.425)
    sun(ax, 7, 0.25)
    sun(ax, 3, 0.1)
    # set axis labels invisible
    ax.set_xticks([])
    ax.set_yticks([])
    plt.show()

if __name__ == "__main__":
    main()