import matplotlib.pyplot as plt

def v(boxes):
    fig, ax = plt.subplots(1)
    ax.set_xlim(-1, 1)
    ax.set_ylim(-1, 1)
    ax.set_xticks([])
    ax.set_yticks([])

    for (x, y, w, h) in boxes:
        rect = plt.Rectangle((x, y), w, h, color='red', fill=False)
        ax.add_patch(rect)
    
    plt.show()